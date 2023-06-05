import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { getBaseUrl } from '../../main';
import { Comment } from '../model/comment';
import { Role } from '../model/enum/role';
import { Publication } from '../model/publication';
import { StorageService } from './storage.service';

@Injectable({
  providedIn: 'root'
})
export class ContentService {

  constructor(private http: HttpClient, private storageService: StorageService) { }

  createNews(news: Publication): Observable<any> {
    return this.http.post(getBaseUrl() + 'content/create-news', news);
  }

  createComment(comment: Comment): Observable<any> {
    return this.http.post(getBaseUrl() + 'content/create-comment-to-news', comment);
  }

  getNews(id: number): Observable<any> {
    const role = this.storageService.getUserRole();
    return this.http.get(getBaseUrl() + 'content/news/' + id);
  }

  getAllNews(): Observable<any> {
    const role = this.storageService.getUserRole();
    if (role != null && role > Role.user) {
      return this.http.get(getBaseUrl() + 'content/all-news');
    } else {
      return this.http.get(getBaseUrl() + 'content/not-hidden-news');
    }
  }

  getAllComments(newsId: number): Observable<any> {
    const role = this.storageService.getUserRole();
    if (role != null && role > Role.user) {
      return this.http.get(getBaseUrl() + 'content/all-comments-to-news/' + newsId);
    } else {
      return this.http.get(getBaseUrl() + 'content/not-hidden-comments-to-news/' + newsId);
    }
  }

  likeNews(news: Publication): Observable<any> {
    return this.http.post(getBaseUrl() + 'content/like-news', news);
  }

  dislikeNews(news: Publication): Observable<any> {
    return this.http.post(getBaseUrl() + 'content/dislike-news', news);
  }

  likeComment(comment: Comment): Observable<any> {
    return this.http.post(getBaseUrl() + 'content/like-comment-to-news', comment);
  }

  dislikeComment(comment: Comment): Observable<any> {
    return this.http.post(getBaseUrl() + 'content/dislike-comment-to-news', comment);
  }
}
