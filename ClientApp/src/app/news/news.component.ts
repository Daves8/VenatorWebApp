import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ContentService } from '../service/content.service';
import { StorageService } from '../service/storage.service';
import { Role } from "../model/enum/role";
import { Publication } from '../model/publication';
import { Comment } from '../model/comment';

@Component({
  selector: 'app-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.css']
})
export class NewsComponent implements OnInit {

  allNews: Publication[] = [];
  news!: Publication;
  allComments: Comment[] = [];
  newsId: number | null = null;
  isModerator: boolean | null = null;
  isLogged: boolean | null = null;
  title!: string;
  isFormVisible: boolean = false;
  newsForCreate: Publication = new Publication();
  commentForCreate: Comment = new Comment();
  error: string = '';

  constructor(private contentService: ContentService, private storageService: StorageService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.applyNewsId();
    const role = this.storageService.getUserRole();
    this.isModerator = role != null && role > Role.user;
    this.isLogged = this.storageService.isAuthorized();
  }

  openForm() {
    this.isFormVisible = true;
    this.title = "Написати новину";
  }

  closeForm() {
    this.isFormVisible = false;
    this.title = "Новини";
    this.newsForCreate.name = '';
    this.newsForCreate.metrics = '';
    this.newsForCreate.text = '';
    this.error = '';
  }

  onSubmitCreateNews() {
    this.contentService.createNews(this.newsForCreate).subscribe(() => {
      this.closeForm();
      this.getAllNews();
    },
      error => this.error = error.error);
  }

  onSubmitCreateComment() {
    this.commentForCreate.parent = this.news;
    this.contentService.createComment(this.commentForCreate).subscribe(() => {
      this.getAllComments(this.news.id);
      this.commentForCreate.text = '';
      this.error = '';
    },
      error => this.error = error.error);
  }

  handleLikeClick(selectedNews: Publication) {
    if (this.storageService.isAuthorized()) {
      this.contentService.likeNews(selectedNews).subscribe(() => {

        if (selectedNews.currentLike) {
          selectedNews.likesCount--;
        } else {
          selectedNews.likesCount++;
        }

        if (selectedNews.currentDislike) {
          selectedNews.dislikesCount--;
        }

        selectedNews.currentLike = !selectedNews.currentLike;
        selectedNews.currentDislike = false;

      });
    }
  }

  handleDislikeClick(selectedNews: Publication) {
    if (this.storageService.isAuthorized()) {
      this.contentService.dislikeNews(selectedNews).subscribe(() => {

        if (selectedNews.currentDislike) {
          selectedNews.dislikesCount--;
        } else {
          selectedNews.dislikesCount++;
        }

        if (selectedNews.currentLike) {
          selectedNews.likesCount--;
        }

        selectedNews.currentDislike = !selectedNews.currentDislike;
        selectedNews.currentLike = false;

      });
    }
  }

  handleLikeClickToComment(selectedComment: Comment) {
    if (this.storageService.isAuthorized()) {
      this.contentService.likeComment(selectedComment).subscribe(() => {

        if (selectedComment.currentLike) {
          selectedComment.likesCount--;
        } else {
          selectedComment.likesCount++;
        }

        if (selectedComment.currentDislike) {
          selectedComment.dislikesCount--;
        }

        selectedComment.currentLike = !selectedComment.currentLike;
        selectedComment.currentDislike = false;

      });
    }
  }

  handleDislikeClickToComment(selectedComment: Comment) {
    if (this.storageService.isAuthorized()) {
      this.contentService.dislikeComment(selectedComment).subscribe(() => {

        if (selectedComment.currentDislike) {
          selectedComment.dislikesCount--;
        } else {
          selectedComment.dislikesCount++;
        }

        if (selectedComment.currentLike) {
          selectedComment.likesCount--;
        }

        selectedComment.currentDislike = !selectedComment.currentDislike;
        selectedComment.currentLike = false;

      });
    }
  }

  applyNewsId(): void {
    this.route.paramMap.subscribe(params => {
      const idStr = params.get('id');
      if (idStr && idStr.length > 0) {
        const id = Number(idStr);
        if (!isNaN(id)) {

          this.newsId = id;
          this.getNews(id);

        } else {
          this.router.navigate(['/news']);
        }
      } else {
        this.getAllNews();
        this.title = "Новини";
      }
    });
  }

  getNews(id: number): void {
    this.contentService.getNews(id).subscribe(response => {
      this.news = response;
      if (!this.news) {
        this.router.navigate(['/news']);
      }
      this.title = this.news.name;
      this.getAllComments(this.news.id);
    }, () => this.router.navigate(['/news']));
  }

  getAllNews(): void {
    this.contentService.getAllNews().subscribe(response => {
      this.allNews = response;
    }, () => this.router.navigate(['/news']));
  }

  getAllComments(newsId: number): void {
    this.contentService.getAllComments(newsId).subscribe(response => {
      this.allComments = response;
    }, () => this.router.navigate(['/news']));
  }
}
