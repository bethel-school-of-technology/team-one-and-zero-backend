using Microsoft.EntityFrameworkCore;
using TEAM_ONE_AND_ZERO_BACKEND.Migrations;
using TEAM_ONE_AND_ZERO_BACKEND.Models;

namespace TEAM_ONE_AND_ZERO_BACKEND.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly PoPDbContext _context;

    public CommentRepository(PoPDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Comment> GetAllComments()
    {
        return _context.Comment.ToList();
    }

    public async Task<IEnumerable<Comment?>> GetCommentsByUsername(string username)
    {
        return await _context.Comment
                .Where(x => x.Username == username)
                .ToListAsync();
    }

    public async Task<IEnumerable<Comment?>> GetCommentsBySongID(string songId)
    {
        return await _context.Comment
                .Where(x => x.SongId == songId)
                .ToListAsync();
    }

    public Comment? GetComment(int commentId)
    {
        return _context.Comment.SingleOrDefault(c => c.CommentID == commentId);
    }

    public Comment CreateComment(Comment newComment)
    {
        _context.Comment.Add(newComment);
        _context.SaveChanges();
        return newComment;
    }

    public Comment? UpdateComment(Comment newComment)
    {
        var originalComment = _context.Comment.Find(newComment.CommentID);

        if (originalComment != null)
        {
            originalComment.Description = newComment.Description;
            _context.SaveChanges();
        }

        return originalComment;
    }

    public void DeleteComment(int commentId)
    {
        var comment = _context.Comment.Find(commentId);

        if(comment != null)
        {
            _context.Comment.Remove(comment);
            _context.SaveChanges();
        }
    }

}